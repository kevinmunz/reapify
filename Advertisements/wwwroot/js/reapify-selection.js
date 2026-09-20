const selection = document.getElementById('enrollment-selection');
const creatorRows = Array.from(selection.querySelectorAll('[data-creator-row]'));
function updateCreatorSelection() {
    const query = document.getElementById('creator-filter').value.trim().toLocaleLowerCase();
    let visible = 0;
    creatorRows.forEach(row => {
        row.hidden = !row.textContent.toLocaleLowerCase().includes(query);
        if (!row.hidden) visible++;
    });
    const count = selection.querySelectorAll('input[name="selectedIds"]:checked').length;
    document.getElementById('selection-count').textContent = `${count} new creators selected. Selections are kept when filtering.`;
    document.getElementById('creator-empty').hidden = visible > 0;
}
document.getElementById('creator-filter').addEventListener('input', updateCreatorSelection);
selection.addEventListener('change', updateCreatorSelection);
updateCreatorSelection();
