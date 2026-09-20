// Enhance opted-in tables; their content remains readable if the library is unavailable.
document.querySelectorAll('[data-reapify-table]').forEach(table => {
    if (window.simpleDatatables) new simpleDatatables.DataTable(table, { perPage: 10, perPageSelect: [10, 25, 50] });
});
