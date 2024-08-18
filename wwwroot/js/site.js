function details(id) {
    let url = 'http://localhost:5223/'
    window.location.href = `${url}Movie/Details/${id}`
};

function watchPremo(src) {
    let myDiv = document.querySelector('.test');
    let video = document.createElement("video");
    video.setAttribute("width", "320");
    video.setAttribute("height", "240");
    video.setAttribute("controls", "controls");
    let srcVideo = document.createElement("source");
    srcVideo.setAttribute("src", `${src}`);
    srcVideo.setAttribute("type", "video/mp4");
    video.appendChild(srcVideo);
    video.style.zIndex = 1000;
    myDiv.appendChild(video);

}

let form = document.getElementById("registerForm")
let labels = document.querySelectorAll("#registerForm .part label")
labels.forEach(l => {
    l.style.cssText=`display:none;`
})
let inpts = document.querySelectorAll("#registerForm input")
inpts.forEach(i => {
    i.onfocus = function () {
        i.removeAttribute("placeholder");
        i.previousElementSibling.style.cssText = "display:block;"
        i.onblur = function () {
            i.previousElementSibling.style.cssText = "display:none;"
            i.setAttribute("placeholder", i.getAttribute("name"))
        }
    }
})

function allConfirm(id) {
    let form = document.getElementById(id);

    fetch(form.action, {
        method: form.method,
        body: new FormData(form),
        headers: { 'X-Requested-With': 'XMLHttpRequest' }
    }).then(response => response.json())
        .then(data => {
            if (data.isvalid) {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Your work has been Done",
                    showConfirmButton: false,
                    timer: 1500
                }).then(() => {
                    form.submit();
                });
            } else {
              
                form.querySelectorAll("span.text-danger").forEach(span => {
                    span.innerHTML = "";
                });

                
                for (let fieldName in data.nameErrors) {
                    let field = document.querySelector(`#${id} #${fieldName}`);
                    if (field) {
                        let span = field.nextElementSibling;
                        if (span && span.classList.contains('text-danger')) {
                            span.innerHTML = data.nameErrors[fieldName].join("<br>");
                        }
                    }
                }
            }
        });
}

function test() {
    document.querySelectorAll(".text-danger").forEach(span => {
        span.innerHTML = "";
    });
}
//Start Section Of Delet Item And Shoe Alert
function deleteIt(itemId, controler) {
    Swal.fire({
        
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Your work has been saved",
                showConfirmButton: false,
                timer: 1500
            }).then(() => {

                window.location.href = `/${controler}/Delete/${itemId}`;

            });
        }
    });
}

/*Edit Function for Movie*/
/*Object*/
function openEditModal(id,controllerName) {
    $.ajax({
        url: `/${controllerName}/Edit/${id}`,
        type: 'GET',
        success: function (data) {
            $('#editMovie .modal-content').html(data);
            $('#editMovie').modal('show');
        }
    });
}





// edit
var editCategoryModal = document.getElementById('editCategoryModal')
editCategoryModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget
    var categoryId = button.getAttribute('data-category-id')
    var categoryName = button.getAttribute('data-category-name')
    var modalTitle = editCategoryModal.querySelector('.modal-title')
    var categoryIdInput = editCategoryModal.querySelector('#editCategoryId')
    var categoryNameInput = editCategoryModal.querySelector('#editCategoryName')

    modalTitle.textContent = 'Edit Category: ' + categoryName
    categoryIdInput.value = categoryId
    categoryNameInput.value = categoryName
})

// delete
var deleteCategoryModal = document.getElementById('deleteCategoryModal')
deleteCategoryModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget
    var categoryId = button.getAttribute('data-category-id')
    var categoryIdInput = deleteCategoryModal.querySelector('#deleteCategoryId')

    categoryIdInput.value = categoryId
})



