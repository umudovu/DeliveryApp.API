$(document).ready(function () {
    let AddCart = $(".addCart");
    let MinusItem = $(".minusCart");
    let RemoveItem = $(".removeCart");
    let ItemsCart = $(".itemsChart");




    //Add to cart

    AddCart.each(function () {
        $(this).on("click", function () {
            
            let dataId = $(this).attr("data-id");
            axios.get('/basket/additem?id=' + dataId)
                .then(function (response) {
                    //$("#HeaderBasketCount").text(response.data.basketCount);
                    //$("#HeaderSubTotal").text(response.data.subTotal);
                    refresh(response.data.products)
                    console.log(response)
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                });
        })
    })

        // MinusItem

    MinusItem.each(function () {
        $(this).on("click", function () {
            let dataId = $(this).attr("data-id");
            axios.get('/basket/itemminus?id=' + dataId)
                .then(function (response) {
                    //$("#HeaderBasketCount").text(response.data.basketCount);
                    //$("#HeaderSubTotal").text(response.data.subTotal);
                    refresh(response.data.products)
                    console.log(response)
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                });

        })
    })

    // Remove item

    RemoveItem.each(function () {
        $(this).on("click", function () {
            let dataId = $(this).attr("data-id");
            axios.get('/basket/itemremove?id=' + dataId)
                .then(function (response) {
                    //$("#HeaderBasketCount").text(response.data.basketCount);
                    //$("#HeaderSubTotal").text(response.data.subTotal);
                    refresh(response.data.products)
                    console.log(response)
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                });

        })
    })


    //refresh

    function refresh(products) {

        location.reload();
       
        //alert(products.forEach(p => {
        //    `
        //    <div class="gold-members d-flex align-items-center justify-content-between px-3 py-2 border-bottom">
        //    <div class="media align-items-center col-8">
        //    <div data-id="${p.id}" style="cursor: pointer;" class="mr-2 text-danger removeCart">&middot;</div>
        //    <div class="media-body">
        //    <p class="m-0">${p.name}</p>
        //    </div>
        //    </div>
        //    <div class="d-flex align-items-center  col-4 justify-content-between">
        //    <span class="count-number float-right d-flex align-items-start justify-content-start">
        //        <button data-id="${p.id}" type="button" class="btn-sm left dec btn btn-outline-secondary minusCart"> <i class="feather-minus"></i> </button>
        //        <input class="count-number-input" type="text" readonly="" value="${p.basketCount}">
        //        <button data-id="${p.id}" type="button" class="btn-sm right inc btn btn-outline-secondary addCart "> <i class="feather-plus"></i> </button>
        //    </span>
        //    <p class="text-gray mb-0 float-right ml-2 text-muted small d-flex align-items-end">
        //        $<span id="productPrice">${p.price}</span>
        //    </p>
        //    </div>
        //    </div>

        //        `

        //}))

        
        
    }
    










});