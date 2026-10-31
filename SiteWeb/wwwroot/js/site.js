// SEMCO — site.js

// ---- Back to top button ----
(function () {
    const btn = document.createElement('button');
    btn.id = 'btn-back-top';
    btn.title = 'Retour en haut';
    btn.innerHTML = '<i class="bi bi-arrow-up"></i>';
    document.body.appendChild(btn);

    window.addEventListener('scroll', function () {
        if (window.scrollY > 300) {
            btn.classList.add('visible');
        } else {
            btn.classList.remove('visible');
        }
    });

    btn.addEventListener('click', function () {
        window.scrollTo({ top: 0, behavior: 'smooth' });
    });
})();

// ---- Auto-fermeture menu mobile au clic sur un lien ----
(function () {
    const navbarCollapse = document.getElementById('navbarSemcoContent');
    if (!navbarCollapse) return;
    navbarCollapse.querySelectorAll('.nav-link').forEach(function (link) {
        link.addEventListener('click', function () {
            const bsCollapse = bootstrap.Collapse.getInstance(navbarCollapse);
            if (bsCollapse) bsCollapse.hide();
        });
    });
})();
