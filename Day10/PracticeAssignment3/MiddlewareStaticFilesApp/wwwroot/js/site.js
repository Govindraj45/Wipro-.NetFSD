const loadedMessage = document.querySelector("#loaded-message");

if (loadedMessage) {
    loadedMessage.textContent = `JavaScript loaded successfully at ${new Date().toLocaleTimeString()}.`;
}
