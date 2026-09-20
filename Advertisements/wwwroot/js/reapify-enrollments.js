let enrollmentRequest = 0;
function enrollmentMessage(message) {
    document.getElementById('enrollment-feedback-message').textContent = message;
    bootstrap.Modal.getOrCreateInstance(document.getElementById('enrollment-feedback')).show();
}
async function checkMetric(campaignCreatorId) {
    const request = ++enrollmentRequest;
    enrollmentMessage('Checking submissions…');
    try {
        const response = await fetch(`/Metrics/Exists?${new URLSearchParams({ campaignCreatorId })}`);
        if (!response.ok) throw new Error();
        const data = await response.json();
        if (request === enrollmentRequest) enrollmentMessage(data.exists ? 'A metric has been submitted for this enrollment.' : 'No metric has been submitted for this enrollment.');
    } catch { if (request === enrollmentRequest) enrollmentMessage('Unable to check submissions. Please try again.'); }
}
async function copyToClipboard(campaignId) {
    try {
        const response = await fetch(`/CampaignCreators/CopyToClipboard?${new URLSearchParams({ campaignId })}`);
        if (!response.ok) throw new Error();
        const data = await response.json();
        await navigator.clipboard.writeText(data.creatorEmails.join(', '));
        enrollmentMessage('Creator emails copied to the clipboard.');
    } catch { enrollmentMessage('Unable to copy creator emails. Check the connection and browser clipboard permissions.'); }
}
