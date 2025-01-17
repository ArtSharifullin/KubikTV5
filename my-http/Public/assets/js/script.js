document.addEventListener('DOMContentLoaded', function () {
    const menuButton = document.querySelector('.header__btn-menu');
    const menu = document.querySelector('.header__menu');

    menuButton.addEventListener('click', function () {
        if (menu.style.display === 'block') {
            menu.style.display = 'none';
        } else {
            menu.style.display = 'block';
        }
    });

    document.getElementById('adminLink').addEventListener('click', function (event) {
        event.preventDefault();
        document.getElementById('popup').style.display = 'flex';
    });

    document.getElementById('stayButton').addEventListener('click', function () {
        // Действие при нажатии на кнопку "Остаться на сайте"
        document.getElementById('popup').style.display = 'none';
    });
});
