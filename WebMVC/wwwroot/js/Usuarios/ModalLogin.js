const modal = document.getElementById('verificationModal');
const closeBtn = document.getElementById('closeModal');
const resendBtn = document.getElementById('resendBtn');
const cooldownText = document.getElementById('cooldownText');
const secondsSpan = document.getElementById('seconds');
const closeBtnRegistro = document.getElementById('closeModalRegistro');
const modalRegistro = document.getElementById('verificationModalRegistro');
const resendBtnRegistro = document.getElementById('resendBtnRegistro');
const cooldownTextRegistro = document.getElementById('cooldownTextRegistro');
const secondsSpanRegistro = document.getElementById('secondsRegistro');

let cooldown = false;
let timer;

function checkAndOpenModal() {
    const usuarioNoVerificado = true;

    if (usuarioNoVerificado && !sessionStorage.getItem('modalShown')) {
        modal.style.display = 'flex';
        sessionStorage.setItem('modalShown', 'true');
    }
}

function checkAndOpenModalRegistro() {
    const usuarioNoVerificado = true;

    if (usuarioNoVerificado && !sessionStorage.getItem('modalRegistroShown')) {
        modalRegistro.style.display = 'flex';
        sessionStorage.setItem('modalRegistroShown', 'true');
    }
}

function closeModal() {
    modal.style.display = 'none';
}

function closeModalRegistro() {
    modalRegistro.style.display = 'none';
}

window.addEventListener('click', (event) => {
    if (event.target === modal) {
        closeModal();
    }
    if (event.target === modalRegistro) {
        closeModalRegistro();
    }
});

closeBtn.addEventListener('click', () => {
    modal.style.display = 'none';
});

closeBtnRegistro.addEventListener('click', () => {
    modalRegistro.style.display = 'none';
});

resendBtn.addEventListener('click', async (e) => {
    e.preventDefault();

    if (!cooldown) {
        startCooldown(60);
        const url = resendBtn.getAttribute('data-url') || resendBtn.getAttribute('href');
        if (!url) {
            console.error('No se encontró URL para reenviar verificación.');
            return;
        }

        try {
            const response = await fetch(url, { method: 'GET', headers: { 'X-Requested-With': 'XMLHttpRequest' } });
            if (response.ok) {
                console.log("Se envió correo de verificación a tu email");
            } else {
                console.error('Respuesta no OK al reenviar verificación:', response.status);
            }
        } catch (err) {
            console.error('Error en fetch de reenviar verificación:', err);
        }
    }
});

resendBtnRegistro.addEventListener('click', async (e) => {
    e.preventDefault();

    if (!cooldown) {
        startCooldownRegistro(60);
        const url = resendBtnRegistro.getAttribute('data-url') || resendBtnRegistro.getAttribute('href');
        if (!url) {
            console.error('No se encontró URL para reenviar verificación.');
            return;
        }

        try {
            const response = await fetch(url, { method: 'GET', headers: { 'X-Requested-With': 'XMLHttpRequest' } });
            if (response.ok) {
                console.log("Se envió correo de verificación a tu email");
            } else {
                console.error('Respuesta no OK al reenviar verificación:', response.status);
            }
        } catch (err) {
            console.error('Error en fetch de reenviar verificación:', err);
        }
    }
});

function startCooldown(seconds) {
    cooldown = true;
    resendBtn.disabled = true;
    cooldownText.style.display = 'block';
    secondsSpan.textContent = seconds;

    timer = setInterval(() => {
        seconds--;
        secondsSpan.textContent = seconds;
        if (seconds <= 0) {
            clearInterval(timer);
            cooldown = false;
            resendBtn.disabled = false;
            cooldownText.style.display = 'none';
        }
    }, 1000);
}

function startCooldownRegistro(seconds) {
    cooldown = true;
    resendBtnRegistro.disabled = true;
    cooldownTextRegistro.style.display = 'block';
    secondsSpanRegistro.textContent = seconds;

    timer = setInterval(() => {
        seconds--;
        secondsSpanRegistro.textContent = seconds;
        if (seconds <= 0) {
            clearInterval(timer);
            cooldown = false;
            resendBtnRegistro.disabled = false;
            cooldownTextRegistro.style.display = 'none';
        }
    }, 1000);
}