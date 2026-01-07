function confirmDelete(userId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',    // Standard Red for Delete
        cancelButtonColor: '#6e7881', // Neutral Gray for Cancel
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            // Sending the delete request via Ajax
            $.ajax({
                url: '/Users/Delete',
                type: 'POST',
                data: { id: userId },
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            title: 'Deleted!',
                            text: response.message,
                            icon: 'success'
                        }).then(() => {
                            location.reload(); // Refresh the table after confirmation
                        });
                    } else {
                        // This will show the "SuperAdmin" restriction message or any server error
                        Swal.fire({
                            title: 'Restricted!',
                            text: response.message,
                            icon: 'error'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: 'Error!',
                        text: 'An unexpected server error occurred.',
                        icon: 'error'
                    });
                }
            });
        }
    })
}