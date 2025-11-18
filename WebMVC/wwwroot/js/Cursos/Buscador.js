document.addEventListener("DOMContentLoaded", () => {
    const searchBox = document.querySelector(".search-box");
    const searchInput = searchBox.querySelector("input");
    const searchButton = searchBox.querySelector(".search-icon");
    const form = searchBox.closest("form");

    const hamburgerBtn = document.getElementById("hamburgerBtn");
    const orderMenu = document.querySelector(".order-menu");
    const orderOptions = document.getElementById("orderOptions");

    let expanded = false;

    searchButton.addEventListener("click", (e) => {
        e.preventDefault();

        const currentWidth = searchBox.offsetWidth;
        searchBox.classList.toggle("expanded");
        const expandedWidth = searchBox.offsetWidth;
        const moveDistance = expandedWidth - currentWidth;

        if (!expanded) {
            searchInput.focus();
            orderMenu.style.transform = `translateX(-${moveDistance}px)`;
        } else {
            searchInput.blur();
            searchInput.value = "";
            orderMenu.style.transform = "translateX(0)";
        }

        expanded = !expanded;
    });

    searchInput.addEventListener("keydown", (e) => {
        if (e.key === "Enter") form.submit();
    });

    hamburgerBtn.addEventListener("click", (e) => {
        e.stopPropagation();
        hamburgerBtn.classList.toggle("active");
        orderOptions.classList.toggle("show");
        orderMenu.style.transform = "translateX(0)";
    });

    //document.querySelectorAll("#orderOptions li").forEach(li => {
    //    li.addEventListener("click", () => {
    //        const opcion = li.dataset.opcion;

    //        let filtro = searchInput.value || new URLSearchParams(window.location.search).get('filtro') || "";

    //        const url = li.dataset.url;

    //        if (url) {
    //            window.location.href = `${url}?filtro=${encodeURIComponent(filtro)}&opcionMenu=${encodeURIComponent(opcion)}`;
    //        } else {
    //            console.error("URL del ActionResult no encontrada en data-url");
    //        }
    //    });
    //});

    document.addEventListener("click", (e) => {
        if (!orderMenu.contains(e.target)) {
            orderOptions.classList.remove("show");
            hamburgerBtn.classList.remove("active");
            orderMenu.style.transform = "translateX(0)";
        }
    });

    // Nombre: A-Z / Z-A
    document.querySelectorAll('li[data-opcion="Nombre"] .submenu button').forEach(btn => {
        btn.addEventListener("click", () => {
            const sort = btn.dataset.sort; // "asc" o "desc"
            const filtro = document.querySelector(".search-box input").value || new URLSearchParams(window.location.search).get('filtro') || "";
            const url = document.querySelector('li[data-opcion="Nombre"]').dataset.url;

            window.location.href = `${url}?filtro=${encodeURIComponent(filtro)}&opcionMenu=Nombre&alfabetico=${sort}`;
        });
    });

    // Calificación
    document.querySelectorAll('li[data-opcion="Calificacion"] .submenu button').forEach(btn => {
        btn.addEventListener("click", () => {
            const rating = btn.dataset.rating; // 1-5
            const filtro = document.querySelector(".search-box input").value || new URLSearchParams(window.location.search).get('filtro') || "";
            const url = document.querySelector('li[data-opcion="Calificacion"]').dataset.url;

            window.location.href = `${url}?filtro=${encodeURIComponent(filtro)}&opcionMenu=Calificacion&calificacion=${rating}`;
        });
    });

    // Docente
    const docenteInput = document.querySelector('li[data-opcion="Docente"] .docente-input');
    const docenteSubmit = document.querySelector('li[data-opcion="Docente"] .docente-submit');

    docenteSubmit.addEventListener("click", () => {
        const docente = docenteInput.value.trim();
        if (!docente) return;

        const filtro = document.querySelector(".search-box input").value || new URLSearchParams(window.location.search).get('filtro') || "";
        const url = document.querySelector('li[data-opcion="Docente"]').dataset.url;

        window.location.href = `${url}?filtro=${encodeURIComponent(filtro)}&opcionMenu=Docente&docente=${encodeURIComponent(docente)}`;
    });

});
