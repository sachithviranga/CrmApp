window.showAlert = (type, title, message) => {
    Swal.fire({
        icon: type,
        title: title,
        text: message,
        showConfirmButton: false,
        timer: 3000
    });
};

window.showConfirm = (title, text) => {
    return Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirm',
        cancelButtonText: 'Cancel'
    }).then(result => result.isConfirmed);
};