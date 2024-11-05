chrome.runtime.onStartup.addListener(() => {
  console.log('Chrome iniciado - limpando token de autenticação.');
  chrome.storage.local.remove('authToken', () => {
    if (chrome.runtime.lastError) {
      console.error('Erro ao remover o token:', chrome.runtime.lastError.message);
    } else {
      console.log('Token removido com sucesso.');
    }
  });
});

chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
    if (message.action === 'sendToAPI') {

        chrome.storage.local.get(['authToken'], (result) => {
            const token = result.authToken;
            if (!token) {
                console.error('Token não encontrado.');
                sendResponse({
                    status: 'erro',
                    mensagem: 'Autenticação necessária'
                });
                return;
            }

            chrome.tabs.query({
                active: true,
                lastFocusedWindow: true
            }, (tabs) => {
                const currentTab = tabs[0];
                if (currentTab) {
                    chrome.scripting.executeScript({
                        target: {tabId: currentTab.id},
                        files: ["scripts/contentScript.js"]
                    }, () => {

                        chrome.tabs.sendMessage(currentTab.id, {
                            action: 'captureData'
                        }, (response) => {
                            // Verifica se os dados foram recebidos corretamente
                            if (!response || !response.dadosCapturados) {
                                console.error('Nenhum dado capturado. Verifique o script de conteúdo.');
                                sendResponse({
                                    status: 'erro',
                                    mensagem: 'Nenhum dado capturado.'
                                });
                                return;
                            }

                            const urlData = {
                                urlPublica: currentTab.url,
                                nome: response.dadosCapturados.nome,
                                descricaoProfissional: response.dadosCapturados.descricaoProfissional,
                                sobre: response.dadosCapturados.sobre,
                                experiencias: response.dadosCapturados.experiencias.map(exp => ({
                                    empresa: exp.empresa,
                                    local: exp.local,
                                    cargos: exp.cargos.map(cargo => ({
                                        titulo: cargo.titulo,
                                        periodo: cargo.periodo,
                                        descricao: cargo.descricao
                                    }))
                                })),
                                formacoes: response.dadosCapturados.formacaoAcademica.map(form => ({
                                    instituicao: form.instituicao,
                                    curso: form.curso,
                                    periodo: form.periodo
                                }))
                            };

                            fetch('https://localhost:8000/api/v1/CandidatoExterno/SalvarDados', {
                                    method: 'POST',
                                    headers: {
                                        'Content-Type': 'application/json',
                                        'Authorization': `Bearer ${token}`
                                    },
                                    body: JSON.stringify(urlData)
                                })
                                .then(res => {
									// Verifique o tipo de conteúdo antes de tentar processar como JSON
									const contentType = res.headers.get('content-type');
									if (!res.ok) {
										throw new Error(`Erro na requisição: ${res.status}`);
									}
									if (contentType && contentType.includes('application/json')) {
										return res.json();
									} else {
										return res.text(); // Retorna como texto se não for JSON
									}
								})
								.then(data => {
									console.log('Resposta da API:', data);
									sendResponse({
										status: 'sucesso',
										dados: data
									});
								})
								.catch(err => {
									console.error('Erro ao enviar para a API:', err);
									sendResponse({
										status: 'erro',
										mensagem: err.message
									});
								});
                        });
                    });
                }
            });
        });

        return true;
    }
});