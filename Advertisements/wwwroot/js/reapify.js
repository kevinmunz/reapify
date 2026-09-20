// Shared behavior for the independently written Reapify layout.
document.addEventListener('DOMContentLoaded', () => {
    if (window.feather) window.feather.replace();
    document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(element => new bootstrap.Tooltip(element));
    document.querySelectorAll('[data-bs-toggle="popover"]').forEach(element => new bootstrap.Popover(element));
});
