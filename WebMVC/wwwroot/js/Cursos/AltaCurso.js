const temaInput = document.getElementById("temaInput");
const agregarTemaBtn = document.getElementById("agregarTema");
const listaTemas = document.getElementById("listaTemas");
const temariosHidden = document.getElementById("temariosHidden");

const difButtons = document.querySelectorAll(".dif-btn");
const difHidden = document.getElementById("dificultad");

// --- Selección de dificultad ---
difButtons.forEach(btn => {
    btn.addEventListener("click", () => {

        // Quitar active de todos
        difButtons.forEach(b => b.classList.remove("active"));

        // Activar el botón clickeado
        btn.classList.add("active");

        // Guardar valor oculto
        difHidden.value = btn.dataset.value;
    });
});

// --- Agregar tema ---
agregarTemaBtn.addEventListener("click", () => {
    const texto = temaInput.value.trim();
    if (texto === "") return;

    const li = document.createElement("li");
    li.innerHTML = `
        ${texto}
        <button type="button" class="eliminar">X</button>
    `;

    listaTemas.appendChild(li);
    temaInput.value = "";

    li.querySelector(".eliminar").addEventListener("click", () => {
        li.remove();
    });
});

// --- Antes de enviar el formulario ---
document.getElementById("cursoForm").addEventListener("submit", function () {

    const contenedor = document.getElementById("temariosContainer");
    contenedor.innerHTML = ""; // limpia por si editaste

    let index = 0;

    document.querySelectorAll("#listaTemas li").forEach(li => {
        const texto = li.childNodes[0].textContent.trim();

        if (texto.length > 0) {

            const input = document.createElement("input");
            input.type = "hidden";
            input.name = `Temarios[${index}].Tema`;
            input.value = texto;

            contenedor.appendChild(input);

            index++;
        }
    });

});

