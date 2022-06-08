/// <summary>
// Удалить запись
/// </summary>
function deleteToDo(i)
{
    // Вызов метода контроллера HomeController для удаления записи по ID
    $.ajax({
        url: 'Home/Delete',
        type: 'POST',
        data: {
            id: i
        },
        success: function () {
            window.location.reload()
        }
    });
}

/// <summary>
/// Заполнить форму
/// </summary>
function populateForm(i)
{
    // Получение данных о записи по ID
    $.ajax({
        url: 'Home/PopulateForm',
        type: 'GET',
        data: {
            id: i
        },
        dataType: 'json',
        success: function (response) {
            // Заполнение формы вверху данными и перезапись атрибутов
            // Кнопка "Добавить" => "Изменить"
            // При нажатии на кнопку вызов метода контроллера не Insert,
            // а Update
            $('#ToDoItem_Name').val(response.name);
            $('#ToDoItem_Id').val(response.id);
            $('#form-button').val("Изменить");
            $("#form-action").attr("action", "/Home/Update");
        }
    });
}