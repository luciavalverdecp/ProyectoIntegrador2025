function scrollChatAlFinal() {
    const chat = document.getElementById("chat-box");
    if (!chat) return;

    // esperar a que el DOM pinte los mensajes
    requestAnimationFrame(() => {
        chat.scrollTop = chat.scrollHeight;
        sessionStorage.removeItem("scrollChatToBottom");
    });
}

document.addEventListener("submit", e => {
    if (e.target.closest("form")) {
        sessionStorage.setItem("scrollChatToBottom", "true");
    }
});
document.addEventListener("DOMContentLoaded", () => {
    if (sessionStorage.getItem("scrollChatToBottom")) {
        scrollChatAlFinal();
    }
});
