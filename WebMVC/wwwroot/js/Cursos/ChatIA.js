const chatBtn = document.getElementById("chat-ia-button");
const chatContainer = document.getElementById("chat-ia-container");
const closeChat = document.getElementById("close-chat");
const sendChat = document.getElementById("send-chat");
const chatInput = document.getElementById("chat-input");
const chatBody = document.getElementById("chat-body");

chatBtn.addEventListener("click", () => {
    chatContainer.classList.toggle("chat-hidden");
});

closeChat.addEventListener("click", () => {
    chatContainer.classList.add("chat-hidden");
});

sendChat.addEventListener("click", enviarMensaje);
chatInput.addEventListener("keypress", e => {
    if (e.key === "Enter") enviarMensaje();
});

function enviarMensaje() {
    const texto = chatInput.value.trim();
    if (!texto) return;

    agregarMensaje(texto, "usuario");
    chatInput.value = "";

    fetch("/IA/Consultar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            mensaje: texto
        })
    })
        .then(res => res.json())
        .then(data => {
            agregarMensaje(data.respuesta, "ia");
        })
        .catch(err => {
            agregarMensaje("Ocurrió un error 😕", "ia");
            console.error(err);
        });
}

function agregarMensaje(texto, tipo) {
    const div = document.createElement("div");
    div.classList.add("mensaje", tipo);
    div.innerText = texto;
    chatBody.appendChild(div);
    chatBody.scrollTop = chatBody.scrollHeight;
}

document.addEventListener("DOMContentLoaded", () => {
    const chatBtn = document.getElementById("chat-ia-button");
    const chatContainer = document.getElementById("chat-ia-container");
    const closeChat = document.getElementById("close-chat");


    chatBtn.addEventListener("click", (e) => {
        e.stopPropagation();
        chatContainer.style.display = "flex"; // abrir
    });


    closeChat.addEventListener("click", (e) => {
        e.stopPropagation();
        chatContainer.style.display = "none"; // 🔥 cerrar forzado
    });
});