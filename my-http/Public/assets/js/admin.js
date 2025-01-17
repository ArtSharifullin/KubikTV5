document.addEventListener('DOMContentLoaded', function () {
    // ���������� ��� ����� ���������� ������������
    document.getElementById('add-user-form').addEventListener('submit', async function (event) {
        event.preventDefault(); // ������������� ����������� �������� �����

        const form = event.target;

        // ��������� ������ �� �����
        const formData = new FormData(form);
        const data = new URLSearchParams(formData).toString();

        try {
            // ��������� AJAX-������
            const response = await fetch(form.action, {
                method: form.method,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: data,
            });

            if (!response.ok) {
                throw new Error('������ ��� �������� ������');
            }

            const result = await response.json();
            if (result == false) {
                throw new Error('This user already in database');
            }

            // ������� ����� ������ �������
            const newRow = document.createElement('tr');
            newRow.innerHTML = `
                <td>${result.Id}</td>
                <td>${result.Login}</td>
                <td>${result.Password}</td>
            `;
            // ������� ������� � ��������� ����� ������
            const table = document.getElementById('userTableBody');
            table.appendChild(newRow);
            document.getElementById('addUserLogin').value = '';
            document.getElementById('addUserPassword').value = '';
            alert('User add succesfully !')
            

        } catch (error) {
            alert('Error: ' + error.message);
        }
    });

    // ���������� ��� ����� ���������� ������
    document.getElementById('add-movie-form').addEventListener('submit', async function (event) {
        event.preventDefault(); // ������������� ����������� �������� �����

        const form = event.target;

        // ��������� ������ �� �����
        const formData = new FormData(form);
        const data = new URLSearchParams(formData).toString();

        try {
            // ��������� AJAX-������
            const response = await fetch(form.action, {
                method: form.method,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: data,
            });

            if (!response.ok) {
                throw new Error('������ ��� �������� ������');
            }

            const result = await response.json();
            if (result == false) {
                throw new Error('This movie already in database');
            }

            // ������� ����� ������ �������
            const newRow = document.createElement('tr');
            newRow.innerHTML = `
                <td>${result.Id}</td>
                <td>${result.Name}</td>
                <td>${result.EnglishName}</td>
                <td>${result.Year}</td>
                <td>${result.Country}</td>
                <td>${result.Genre}</td>
                <td>${result.Director}</td>
                <td>${result.Actors}</td>
                <td>${result.Description}</td>
                <td>${result.MovieLink}</td>
                <td>${result.ImageLink}</td>
                <td>${result.Link}</td>
            `;
            // ������� ������� � ��������� ����� ������
            const table = document.getElementById('movieTableBody');
            table.appendChild(newRow);
            document.getElementById('addMovieName').value = '';
            document.getElementById('addMovieEnglishName').value = '';
            document.getElementById('addMovieYear').value = '';
            document.getElementById('addMovieCountry').value = '';
            document.getElementById('addMovieGenre').value = '';
            document.getElementById('addMovieDirector').value = '';
            document.getElementById('addMovieActors').value = '';
            document.getElementById('addMovieDescription').value = '';
            document.getElementById('addMovieLink').value = '';
            document.getElementById('addMovieImageLink').value = '';

            alert('Movie add succesfully !')

        } catch (error) {
        }
    });

    // ���������� ��� ����� �������� ������������
    document.getElementById('delete-user-form').addEventListener('submit', async function (event) {
        event.preventDefault(); // ������������� ����������� �������� �����

        const form = event.target;

        // ��������� ������ �� �����
        const formData = new FormData(form);
        const data = new URLSearchParams(formData).toString();

        try {
            // ��������� AJAX-������
            const response = await fetch(form.action, {
                method: form.method,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: data,
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error('������ ��� �������� ������: ' + errorText);
            }

            const result = await response.json();
            if (result == false) {
                throw new Error('This user not exist');
            }
            // ���������, ���� �� ������ � ������
            if (result.error) {
                alert('Error. Incorrect ID');
                return;
            }

            // ������� �������
            const tableBody = document.getElementById('userTableBody');
            tableBody.innerHTML = '';

            // ��������� ������� ������ �������
            result.forEach(user => {
                const newRow = document.createElement('tr');
                newRow.innerHTML = `
                    <td>${user.Id}</td>
                    <td>${user.Login}</td>
                    <td>${user.Password}</td>
                `;
                tableBody.appendChild(newRow);

            });
            alert('User delete succesfully !')

        } catch (error) {
            alert('Error: ' + error.message);
        }
    });


    document.getElementById('delete-movie-form').addEventListener('submit', async function (event) {
        event.preventDefault(); // ������������� ����������� �������� �����

        const form = event.target;

        // ��������� ������ �� �����
        const formData = new FormData(form);
        const data = new URLSearchParams(formData).toString();

        try {
            // ��������� AJAX-������
            const response = await fetch(form.action, {
                method: form.method,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: data,
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error('������ ��� �������� ������: ' + errorText);
            }

            const result = await response.json();

            if (result == false) {
                throw new Error('This movie not exist');
            }

            // ���������, ���� �� ������ � ������
            if (result.error) {
                alert('Error. Incorrect ID');
                return;
            }

            // ������� �������
            const tableBody = document.getElementById('movieTableBody');
            tableBody.innerHTML = '';

            // ��������� ������� ������ �������
            result.forEach( movie => {
                const newRow = document.createElement('tr');
                newRow.innerHTML = `
                    <td>${movie.Id}</td>
                    <td>${movie.Name}</td>
                    <td>${movie.EnglishName}</td>
                    <td>${movie.Year}</td>
                    <td>${movie.Country}</td>
                    <td>${movie.Genre}</td>
                    <td>${movie.Director}</td>
                    <td>${movie.Actors}</td>
                    <td>${movie.Description}</td>
                    <td>${movie.MovieLink}</td>
                    <td>${movie.ImageLink}</td>
                    <td>${movie.Link}</td>
                `;
                tableBody.appendChild(newRow);
            });
            alert('Movie delete succesfully !')
        } catch (error) {
            alert('Error: ' + error.message);
        }
    });

    document.getElementById('update-user-form').addEventListener('submit', async function (event) {
        event.preventDefault(); // ������������� ����������� �������� �����

        const form = event.target;

        // ��������� ������ �� �����
        const formData = new FormData(form);
        const data = new URLSearchParams(formData).toString();

        try {
            // ��������� AJAX-������
            const response = await fetch(form.action, {
                method: form.method,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: data,
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error('������ ��� �������� ������: ' + errorText);
            }

            const result = await response.json();
            if (result == false) {
                throw new Error('This user not exist');
            }
            // ���������, ���� �� ������ � ������
            if (result.error) {
                alert('Error. Incorrect ID');
                return;
            }

            // ������� �������
            const tableBody = document.getElementById('userTableBody');
            tableBody.innerHTML = '';

            // ��������� ������� ������ �������
            result.forEach(user => {
                const newRow = document.createElement('tr');
                newRow.innerHTML = `
                    <td>${user.Id}</td>
                    <td>${user.Login}</td>
                    <td>${user.Password}</td>
                `;
                tableBody.appendChild(newRow);

            });
            alert('User update succesfully !')

        } catch (error) {
            alert('Error: ' + error.message);
        }
    });

    document.getElementById('update-movie-form').addEventListener('submit', async function (event) {
        event.preventDefault(); // ������������� ����������� �������� �����

        const form = event.target;

        // ��������� ������ �� �����
        const formData = new FormData(form);
        const data = new URLSearchParams(formData).toString();

        try {
            // ��������� AJAX-������
            const response = await fetch(form.action, {
                method: form.method,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: data,
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error('������ ��� �������� ������: ' + errorText);
            }

            const result = await response.json();

            if (result == false) {
                throw new Error('This movie not exist');
            }

            // ���������, ���� �� ������ � ������
            if (result.error) {
                alert('Error. Incorrect ID');
                return;
            }

            // ������� �������
            const tableBody = document.getElementById('movieTableBody');
            tableBody.innerHTML = '';

            // ��������� ������� ������ �������
            result.forEach(movie => {
                const newRow = document.createElement('tr');
                newRow.innerHTML = `
                    <td>${movie.Id}</td>
                    <td>${movie.Name}</td>
                    <td>${movie.EnglishName}</td>
                    <td>${movie.Year}</td>
                    <td>${movie.Country}</td>
                    <td>${movie.Genre}</td>
                    <td>${movie.Director}</td>
                    <td>${movie.Actors}</td>
                    <td>${movie.Description}</td>
                    <td>${movie.MovieLink}</td>
                    <td>${movie.ImageLink}</td>
                    <td>${movie.Link}</td>
                `;
                tableBody.appendChild(newRow);
            });
            alert('Movie update succesfully !')

        } catch (error) {
            alert('Error: ' + error.message);
        }
    });
});
