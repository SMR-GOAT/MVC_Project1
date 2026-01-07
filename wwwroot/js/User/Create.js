document.querySelector('form').addEventListener('submit', function (e) {
    // التحقق من صحة الفورم برمجياً قبل تغيير شكل الزر
    if ($(this).valid()) { 
        const btn = this.querySelector('button[type="submit"]');
        btn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Creating...';
        btn.classList.add('disabled');
    } else {
        // إذا كان هناك خطأ، لا تفعل شيئاً واترك رسائل الخطأ تظهر
        e.preventDefault();
    }
});

function togglePassVisibility() {
    const input = document.getElementById('passInput');
    const icon = document.getElementById('toggleIcon');
    if (input.type === "password") {
        input.type = "text";
        icon.classList.replace('bi-eye-slash', 'bi-eye');
    } else {
        input.type = "password";
        icon.classList.replace('bi-eye', 'bi-eye-slash');
    }
}
