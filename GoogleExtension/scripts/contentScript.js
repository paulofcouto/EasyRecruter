function capturarPerfilCompleto() {
  const perfil = {};

  perfil.URL = window.location.href;

  const nomeElement = document.querySelector('.text-heading-xlarge');
  perfil.nome = nomeElement ? nomeElement.innerText.trim() : 'Nome não encontrado';

  const cargoElemento = document.querySelector('.text-body-medium.break-words');
  perfil.cargo = cargoElemento ? cargoElemento.innerText.trim() : 'Cargo não encontrado';

  perfil.sobre = capturarTextoComQuebras();

  perfil.experiencias = capturarExperiencias();

  perfil.formacaoAcademica = capturarFormacaoAcademica();

  return perfil;
}

function capturarTextoComQuebras() {
  const section = Array.from(document.querySelectorAll('section.artdeco-card.pv-profile-card.break-words.mt2'))
    .find(sec => sec.querySelector('div#about.pv-profile-card__anchor'));

  if (section) {
    const divPrincipal = section.querySelector('div.display-flex.ph5.pv3');
    if (divPrincipal) {
      const divSecundaria = divPrincipal.querySelector('div.display-flex.full-width');
      if (divSecundaria) {
        const spanVisuallyHidden = divSecundaria.querySelector('span.visually-hidden');
        if (spanVisuallyHidden) {
          const texto = spanVisuallyHidden.innerHTML.replace(/<br\s*[\/]?>/gi, '\n');
          return texto.trim();
        }
      }
    }
  }
  return 'Sobre não encontrado';
}

function capturarExperiencias() {
  const experiencias = [];
  const sectionExperiencias = Array.from(document.querySelectorAll('section.artdeco-card.pv-profile-card.break-words.mt2'))
    .find(sec => sec.querySelector('div#experience.pv-profile-card__anchor'));

  if (sectionExperiencias) {
    const divsDentroDaSection = sectionExperiencias.children;
    if (divsDentroDaSection.length >= 3) {
      const terceiraDiv = divsDentroDaSection[2];
      const ulExperiencias = terceiraDiv.querySelector('ul');

      if (ulExperiencias) {
        const listaLi = Array.from(ulExperiencias.children).filter(li => li.tagName.toLowerCase() === 'li');
        listaLi.forEach((li) => {
          const experiencia = {};

          const cargoElement = li.querySelector('.display-flex.align-items-center.mr1.t-bold span.visually-hidden');
          experiencia.cargo = cargoElement ? cargoElement.innerText.trim() : 'Cargo não encontrado';

          const empresaElement = li.querySelector('.display-flex.flex-column.full-width .t-14.t-normal span.visually-hidden');
          experiencia.empresa = empresaElement ? empresaElement.innerText.trim() : 'Empresa não encontrada';

          const dataElement = li.querySelector('.display-flex.flex-column.full-width .t-14.t-normal.t-black--light span.visually-hidden');
          experiencia.periodo = dataElement ? dataElement.innerText.trim() : 'Período não encontrado';

          const spansLocal = li.querySelectorAll('.display-flex.flex-column.full-width .t-14.t-normal.t-black--light span.visually-hidden');
          experiencia.local = spansLocal.length > 1 ? spansLocal[1].innerText.trim() : 'Local não encontrado';

          const atribuicaoElement = li.querySelector('.pvs-entity__sub-components span.visually-hidden');
          experiencia.descricao = atribuicaoElement ? atribuicaoElement.innerText.trim() : 'Descrição não encontrada';

          experiencias.push(experiencia);
        });
      }
    }
  }
  return experiencias;
}

// Função para capturar a Formação Acadêmica
function capturarFormacaoAcademica() {
  const formacaoAcademica = [];
  const sectionFormacao = Array.from(document.querySelectorAll('section.artdeco-card.pv-profile-card.break-words.mt2'))
    .find(sec => sec.querySelector('div#education.pv-profile-card__anchor'));

  if (sectionFormacao) {
    const divsDentroDaSection = sectionFormacao.children;
    if (divsDentroDaSection.length >= 3) {
      const terceiraDiv = divsDentroDaSection[2];
      const ulFormacao = terceiraDiv.querySelector('ul');

      if (ulFormacao) {
        const listaLi = Array.from(ulFormacao.children).filter(li => li.tagName.toLowerCase() === 'li');
        listaLi.forEach((li) => {
          const formacao = {};

          // Captura a Instituição
          const instituicaoElement = li.querySelector('.display-flex.align-items-center.mr1.t-bold span.visually-hidden');
          formacao.instituicao = instituicaoElement ? instituicaoElement.innerText.trim() : 'Instituição não encontrada';

          // Captura o Curso
          const cursoElement = li.querySelector('.display-flex.flex-column.full-width .t-14.t-normal span.visually-hidden');
          formacao.curso = cursoElement ? cursoElement.innerText.trim() : 'Curso não encontrado';

          // Captura o Período
          const periodoElement = li.querySelector('.display-flex.flex-column.full-width .t-14.t-normal.t-black--light span.visually-hidden');
          formacao.periodo = periodoElement ? periodoElement.innerText.trim() : 'Período não encontrado';

          formacaoAcademica.push(formacao);
        });
      }
    }
  }
  return formacaoAcademica;
}

// Captura o perfil completo
const perfilCompleto = capturarPerfilCompleto();

// Enviar os dados capturados de volta para o background.js
chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
  if (message.action === 'captureData') {
    sendResponse({ dadosCapturados: perfilCompleto });
  }
});
