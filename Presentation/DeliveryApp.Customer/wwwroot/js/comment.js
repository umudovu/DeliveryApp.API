
var commentarea = document.getElementById("commentarea");
var addBtn = document.getElementById("addbtncomment");
var content = document.getElementById("contentComment");

addBtn.addEventListener("click", function (e) {
    e.preventDefault();
    var restId = content.getAttribute("data-id");


    axios.post(`/company/addcomment?restId=${restId}&content=${content.value}`)
        .then(function (response) {
            // handle success
            console.log(response);
            var div = document.createElement("div");
            div.innerHTML = response.data;
            //commentarea.innerHTML += response.data;
            commentarea.insertBefore(div, commentarea.firstChild);
        })
        .catch(function (error) {
            // handle error
            console.log(error);
        })
})