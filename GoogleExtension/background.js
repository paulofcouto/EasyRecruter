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
				'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI2Njc2MmFmMWMxMjc1ZDQyOWM1ZDg2MGIiLCJlbWFpbCI6InBhdWxvZmVybmFuZGVzY291dG9AZ21haWwuY29tIiwibmJmIjoxNzMwMDYyODYwLCJleHAiOjE3MzAxMDYwNjAsImlhdCI6MTczMDA2Mjg2MH0.rr11fmjh8cmHjoPBiqsFYgdFAlOAUqAlII4HGlWJLdY'
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
