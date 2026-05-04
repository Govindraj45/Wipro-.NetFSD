const button = document.querySelector("#messageButton");
const message = document.querySelector("#message");

if (button && message) {
    button.addEventListener("click", () => {
        message.textContent = "Static JavaScript loaded successfully from wwwroot.";
    });
}
