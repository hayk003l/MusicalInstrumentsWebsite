window.authTabs = {
    setActiveTab: function (tabName) {
        const loginBtn = document.querySelector('[data-tab="login"]');
        const registerBtn = document.querySelector('[data-tab="register"]');
        const loginForm = document.querySelector('[data-form="login"]');
        const registerForm = document.querySelector('[data-form="register"]');

        if (!loginBtn || !registerBtn || !loginForm || !registerForm) {
            console.warn("Auth elements not found!");
            return;
        }

        if (tabName === "login") {
            loginBtn.classList.add("active");
            registerBtn.classList.remove("active");
            loginForm.style.display = "flex";
            registerForm.style.display = "none";
        } else {
            registerBtn.classList.add("active");
            loginBtn.classList.remove("active");
            registerForm.style.display = "flex";
            loginForm.style.display = "none";
        }
    },

    init: function () {
        this.setActiveTab("login"); // default to login tab
    }
};
