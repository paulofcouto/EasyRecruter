chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
  if (message.action === 'sendToAPI') {
    chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
		
	  const currentTab = tabs[0];
	  console.log(currentTab)
      if (currentTab) {

        chrome.scripting.executeScript({
          target: { tabId: currentTab.id },
          "files": ["scripts/contentScript.js"]
        }, () => {

          chrome.tabs.sendMessage(currentTab.id, { action: 'captureData' }, (response) => {
			
			console.log(response.dadosCapturados.nome);
            const urlData = {
              url: currentTab.url,
              nome: response.dadosCapturados.nome,
              cargo: response.dadosCapturados.cargo,
              sobre: response.dadosCapturados.sobre,
              experiencias: response.dadosCapturados.experiencias.map(exp => ({
                cargo: exp.cargo,
                empresa: exp.empresa,
                periodo: exp.periodo,
                local: exp.local,
                descricao: exp.descricao
              })), 
              formacaoAcademica: response.dadosCapturados.formacaoAcademica.map(form => ({
                instituicao: form.instituicao,
                curso: form.curso,
                periodo: form.periodo
              })) 
            };

            fetch('http://localhost:5214/SalvarUrl', {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(urlData)
            })
            .then(res => {
              if (!res.ok) {
                throw new Error(`Erro na requisição: ${res.status}`);
              }
              return res.json(); 
            })
            .then(data => {
              console.log('Resposta da API:', data);
              sendResponse({ status: 'sucesso', dados: data });
            })
            .catch(err => {
              console.error('Erro ao enviar para a API:', err);
              sendResponse({ status: 'erro', mensagem: err.message });
            });
          });
        });
      }
    });
    return true;
  }
});
