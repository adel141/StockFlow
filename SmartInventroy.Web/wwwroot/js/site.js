// StockFlow preferences are applied before interactive page features initialize.
(function applyStockFlowPreferences() {
    try {
        const root = document.documentElement;
        const theme = localStorage.getItem('stockflow-theme') || 'light';
        const accent = localStorage.getItem('stockflow-accent') || '#477fe5';
        const density = localStorage.getItem('stockflow-table-density') || 'comfortable';
        root.dataset.theme = theme;
        root.dataset.tableDensity = density;
        root.style.setProperty('--sf-primary', accent);
        if (theme === 'dark' || (theme === 'system' && window.matchMedia('(prefers-color-scheme: dark)').matches)) root.classList.add('theme-dark');
    } catch (error) { /* Preferences are optional; the default theme remains available. */ }
})();

document.addEventListener("DOMContentLoaded", function () {
    const appShell = document.querySelector(".app-shell");
    const sidebar = document.getElementById("appSidebar");
    const toggle = document.getElementById("sidebarToggle");
    let backdrop = document.querySelector(".sidebar-backdrop");

    if (sidebar && !backdrop) {
        backdrop = document.createElement("button");
        backdrop.type = "button";
        backdrop.className = "sidebar-backdrop";
        backdrop.setAttribute("aria-label", "Close navigation");
        document.body.appendChild(backdrop);
    }

    function closeSidebar() {
        if (!sidebar) return;
        sidebar.classList.remove("sidebar-open");
        if (backdrop) backdrop.classList.remove("show");
        if (toggle) toggle.setAttribute("aria-expanded", "false");
    }

    if (toggle && sidebar) {
        toggle.setAttribute("aria-expanded", "false");
        toggle.addEventListener("click", function () {
            if (window.matchMedia("(max-width: 768px)").matches) {
                const opened = sidebar.classList.toggle("sidebar-open");
                if (backdrop) backdrop.classList.toggle("show", opened);
                toggle.setAttribute("aria-expanded", String(opened));
            } else if (appShell) {
                appShell.classList.toggle("sidebar-collapsed");
                const collapsed = appShell.classList.contains("sidebar-collapsed");
                toggle.setAttribute("aria-expanded", String(!collapsed));
                try {
                    if (localStorage.getItem("stockflow-remember-sidebar") !== "false") localStorage.setItem("stockflow-sidebar-collapsed", String(collapsed));
                } catch (error) {
                    // Navigation still works when browser storage is unavailable.
                }
            }
        });
        try {
            if (appShell && window.matchMedia("(min-width: 769px)").matches && localStorage.getItem("stockflow-remember-sidebar") !== "false" && localStorage.getItem("stockflow-sidebar-collapsed") === "true") {
                appShell.classList.add("sidebar-collapsed");
            }
        } catch (error) {
            // Ignore storage restrictions; the menu remains interactive.
        }
        if (backdrop) backdrop.addEventListener("click", closeSidebar);
        window.addEventListener("resize", function () {
            if (!window.matchMedia("(max-width: 768px)").matches) closeSidebar();
        });
    }

    // Account menu: keyboard and pointer accessible profile actions.
    const profileToggle = document.getElementById('profileToggle');
    const profileMenu = document.getElementById('profileMenu');
    function closeProfileMenu() {
        if (!profileMenu || !profileToggle) return;
        profileMenu.hidden = true;
        profileToggle.setAttribute('aria-expanded', 'false');
    }
    if (profileToggle && profileMenu) {
        profileToggle.addEventListener('click', function (event) {
            event.stopPropagation();
            const opening = profileMenu.hidden;
            profileMenu.hidden = !opening;
            profileToggle.setAttribute('aria-expanded', String(opening));
        });
        profileMenu.addEventListener('click', function (event) { event.stopPropagation(); });
        document.addEventListener('click', closeProfileMenu);
        document.addEventListener('keydown', function (event) {
            if (event.key === 'Escape') closeProfileMenu();
        });
    }

    // Notification popover: load on open, refresh count on page load, close on outside click / Escape.
    const notificationToggle = document.getElementById("notificationToggle");
    const notificationPopover = document.getElementById("notificationPopover");
    const notificationCount = document.getElementById("notificationCount");
    async function loadNotifications() {
        if (!notificationPopover) return;
        notificationPopover.innerHTML = '<div class="notification-loading">Loading notifications…</div>';
        try {
            const response = await fetch('/Notifications/Panel', { headers: { 'X-Requested-With': 'XMLHttpRequest' }, credentials: 'same-origin' });
            if (!response.ok) throw new Error('Unable to load notifications');
            notificationPopover.innerHTML = await response.text();
            const content = notificationPopover.querySelector('[data-count]');
            const count = content ? Number(content.dataset.count) : 0;
            if (notificationCount) {
                notificationCount.textContent = count > 99 ? '99+' : String(count);
                notificationCount.hidden = count === 0;
            }
        } catch {
            notificationPopover.innerHTML = '<div class="notification-loading text-danger">Notifications could not be loaded. Try again.</div>';
        }
    }
    if (notificationToggle && notificationPopover) {
        loadNotifications();
        notificationToggle.addEventListener('click', async function (event) {
            event.stopPropagation();
            const opening = notificationPopover.hidden;
            notificationPopover.hidden = !opening;
            notificationToggle.setAttribute('aria-expanded', String(opening));
            if (opening) await loadNotifications();
        });
        document.addEventListener('click', function (event) {
            if (!notificationPopover.contains(event.target) && !notificationToggle.contains(event.target)) {
                notificationPopover.hidden = true;
                notificationToggle.setAttribute('aria-expanded', 'false');
            }
        });
        document.addEventListener('keydown', function (event) {
            if (event.key === 'Escape') {
                notificationPopover.hidden = true;
                notificationToggle.setAttribute('aria-expanded', 'false');
                closeSidebar();
                closeProfileMenu();
            }
        });
    }

    // Reusable client-side search, sort and pagination for all listing tables.
    document.querySelectorAll('table.js-data-table').forEach(function (table) {
        const tbody = table.tBodies[0];
        if (!tbody) return;
        const rows = Array.from(tbody.rows).filter(row => !row.querySelector('td[colspan]'));
        if (rows.length === 0) return;
        const wrapper = table.closest('.content-card') || table.parentElement;
        const controls = document.createElement('div');
        controls.className = 'table-tools d-flex flex-wrap gap-2 justify-content-between align-items-center mb-3';
        controls.innerHTML = '<div class="input-group table-search"><span class="input-group-text"><i class="bi bi-search"></i></span><input type="search" class="form-control" placeholder="Search this table…" aria-label="Search table"></div><label class="table-page-size">Rows <select class="form-select form-select-sm"><option>10</option><option>25</option><option>50</option><option>100</option></select></label>';
        wrapper.insertBefore(controls, table.parentElement === wrapper ? table : table.parentElement);
        const search = controls.querySelector('input');
        const size = controls.querySelector('select');
        try { const preferredSize = localStorage.getItem('stockflow-page-size'); if (preferredSize && ['10', '25', '50', '100'].includes(preferredSize)) size.value = preferredSize; } catch (error) { /* Keep default page size. */ }
        const pager = document.createElement('div');
        pager.className = 'table-pagination d-flex flex-wrap justify-content-between align-items-center gap-2 mt-3';
        pager.innerHTML = '<small class="text-muted table-result-count"></small><div class="btn-group btn-group-sm" role="group"><button type="button" class="btn btn-outline-secondary" data-page="prev">Previous</button><button type="button" class="btn btn-outline-secondary" data-page="next">Next</button></div>';
        table.parentElement.insertAdjacentElement('afterend', pager);
        let page = 1;
        let sortIndex = -1;
        let sortDirection = 1;
        const headers = Array.from(table.tHead?.rows[0]?.cells || []);
        headers.forEach((header, index) => {
            if (header.dataset.noSort === 'true' || /actions/i.test(header.textContent)) return;
            header.classList.add('sortable-header');
            header.title = 'Sort by ' + header.textContent.trim();
            header.addEventListener('click', function () {
                if (sortIndex === index) sortDirection *= -1;
                else { sortIndex = index; sortDirection = 1; }
                rows.sort((a, b) => {
                    const av = (a.cells[index]?.innerText || '').trim();
                    const bv = (b.cells[index]?.innerText || '').trim();
                    const an = Number(av.replace(/[^0-9.-]/g, ''));
                    const bn = Number(bv.replace(/[^0-9.-]/g, ''));
                    const compare = av !== '' && bv !== '' && !Number.isNaN(an) && !Number.isNaN(bn)
                        ? an - bn : av.localeCompare(bv, undefined, { numeric: true, sensitivity: 'base' });
                    return compare * sortDirection;
                });
                rows.forEach(row => tbody.appendChild(row));
                render();
            });
        });
        function render() {
            const term = search.value.trim().toLowerCase();
            const filtered = rows.filter(row => row.innerText.toLowerCase().includes(term));
            const pageSize = Number(size.value);
            const pageCount = Math.max(1, Math.ceil(filtered.length / pageSize));
            page = Math.min(page, pageCount);
            const start = (page - 1) * pageSize;
            rows.forEach(row => row.hidden = true);
            filtered.slice(start, start + pageSize).forEach(row => row.hidden = false);
            pager.querySelector('.table-result-count').textContent = filtered.length ? `Showing ${start + 1}–${Math.min(start + pageSize, filtered.length)} of ${filtered.length} records` : 'No matching records';
            pager.querySelector('[data-page="prev"]').disabled = page <= 1;
            pager.querySelector('[data-page="next"]').disabled = page >= pageCount;
        }
        search.addEventListener('input', function () { page = 1; render(); });
        size.addEventListener('change', function () { page = 1; render(); });
        pager.querySelector('[data-page="prev"]').addEventListener('click', function () { page--; render(); });
        pager.querySelector('[data-page="next"]').addEventListener('click', function () { page++; render(); });
        render();
    });
});
