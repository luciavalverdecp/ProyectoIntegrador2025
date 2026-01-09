function scrollChatAlFinal() {
    const chat = document.getElementById("chat-box");
    if (!chat) return;

    chat.scrollTop = chat.scrollHeight;
}

fetch(url)
    .then(r => r.text())
    .then(html => {
        document.getElementById("chatPanel").innerHTML = html;
        scrollChatAlFinal(); 
    });
