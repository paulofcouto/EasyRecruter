chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
  if (message.action === 'sendToAPI') {
    chrome.identity.getProfileUserInfo((userInfo) => {
		
      if (userInfo.email != '') {
        chrome.tabs.query({ active: true, currentWindow: true }, async (tabs) => {
          const currentTab = tabs[0];
          if (currentTab) {
            const urlData = {
              url: currentTab.url,
              usuario: userInfo.email
            };
            try {
              const response = await fetch('http://localhost:5214/SalvarUrl', {
                method: 'POST',
                headers: {
                  'Content-Type': 'application/json'
                },
                body: JSON.stringify(urlData)
              });
              
              sendResponse({ status: response.status });
            } 
            catch (error) {
              sendResponse({ status: error });
            }
          }
        });
      } else {
        sendResponse({ status: 'Usuário não identificado' });
      }
    });
    return true; 
  }
});
