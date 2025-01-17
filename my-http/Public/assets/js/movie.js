
//document.addEventListener('DOMContentLoaded', function() {
//    // Получаем все ссылки с классом movieLink
//    const movieLinks = document.querySelectorAll('.movieLink');

//    // Добавляем обработчик события click для каждой ссылки
//    movieLinks.forEach(function(link) {
//        link.addEventListener('click', function (event) {
//            // Предотвращаем стандартное поведение ссылки
//            event.preventDefault();

//            // Получаем значение movie.Name из элемента
//            const movieName = link.querySelector('.item-poster__title').innerHTML;

//            // Отправляем значение movie.Name на эндпоинт /movie
//            fetch('/movie?movieName=' + movieName, {
//                headers: {
//                    'Content-Type': 'application/json'
//                },
//            })
//                .then(response => response.json())
//                .then(data => {
//                    console.log('Success:', data);
//                    // Здесь можно добавить код для обработки успешного ответа
//                })
//                .catch((error) => {
//                    console.error('Error:', error);
//                    // Здесь можно добавить код для обработки ошибки
//                });
//        });
//    });
//});


