// wwwroot/js/User/UserEdit.js
function togglePassVisibility() {
    const passInput = document.getElementById('passInput');
    const toggleIcon = document.getElementById('toggleIcon');
    
    if (passInput.type === 'password') {
        passInput.type = 'text';
        toggleIcon.classList.replace('bi-eye-slash', 'bi-eye');
    } else {
        passInput.type = 'password';
        toggleIcon.classList.replace('bi-eye', 'bi-eye-slash');
    }
}