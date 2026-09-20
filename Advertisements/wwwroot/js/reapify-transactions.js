let transactionLookupRequest = 0;
async function checkTransaction(entityId, productId) {
    const request = ++transactionLookupRequest;
    const result = document.getElementById('transaction-lookup-result');
    result.textContent = 'Checking transaction records…';
    bootstrap.Modal.getOrCreateInstance(document.getElementById('transaction-lookup')).show();
    try {
        const response = await fetch(`/Transactions/Exists?${new URLSearchParams({ entityId, productId })}`);
        if (!response.ok) throw new Error('Transaction lookup failed');
        const data = await response.json();
        if (request === transactionLookupRequest) result.textContent = data.exists ? 'A transaction is recorded for this entity and record.' : 'No transaction is recorded for this entity and record.';
    } catch {
        if (request === transactionLookupRequest) result.textContent = 'Unable to check transactions. Close this dialog and try again.';
    }
}
