window.addAccordionOnClickHandlers = (function() {
    const acc = document.getElementsByClassName("accordion");
    
    return function() {
        for (let i = 0; i < acc.length; i++) {
            acc[i].addEventListener("click", function () {
                this.classList.toggle("active");
                const panel = this.nextElementSibling;

                if (panel.style.display === "") {
                    panel.style.display = "none";
                } else {
                    panel.style.display = "";
                }
            });
        }
    }
})();