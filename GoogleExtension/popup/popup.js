document.addEventListener('DOMContentLoaded', () => {
  const sendButton = document.getElementById('send');
  const successMessage = document.getElementById('success-message');

  sendButton.addEventListener('click', () => {
    chrome.runtime.sendMessage({ action: 'sendToAPI' }, (response) => {
      console.log('Resposta do background: ', response.status);
      
      if (response.status === 201) { 
        successMessage.style.display = 'block';
        sendButton.style.display = 'none'; 
      }
    });
  });
});
