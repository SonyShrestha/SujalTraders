var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "productCode", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "availableQuantity", "width": "15%" },
            { "data": "unitPrice", "width": "15%" },
            { "data": "productSubCategory.name", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <td>
                        <a class="btn btn-outline-dark" href="/Admin/Product/Upsert?id=${data}">Edit</a>
                        <a onclick=Delete('/Admin/Product/Delete/'+${data}) class="btn btn-outline-dark">Delete</a>
                    </td>
                    `
                },
                "width": "15%"

            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}