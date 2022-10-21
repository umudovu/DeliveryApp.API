let addressSearch = document.getElementById("addressSearch");
let listGroup = document.getElementById("listArea");
let setaddressbtn = document.getElementById("setaddressbtn");
let localization = document.querySelectorAll(".localization");


addressSearch.onkeyup = function () {


    let value = "";
    let location = this.value;

    axios.get('https://maps.googleapis.com/maps/api/geocode/json', {
        params: {
            address: location,
            key: 'AIzaSyCFpqjXIt_kVyWqK3_9mrCXD5_hASViWus'
        }
    })
        .then(function (response) {
            // handle success
            console.log(response);

            for (var i = 0; i < response.data.results.length; i++) {
                
                var formattedAddress = response.data.results[i].formatted_address;
                var lat = response.data.results[i].geometry.location.lat;
                var lng = response.data.results[i].geometry.location.lng;

                $("#listArea a").slice(0).remove();
                listGroup.innerHTML = `
                                     <button type="button" data-dismiss="modal" class="list-group-item mb-2 localization list-group-item-action active">
												<i class="icon-copy fa fa-location-arrow mx-2" aria-hidden="true"></i>
												${formattedAddress}</a>

            `


                setaddressbtn.addEventListener("click", function () {
                  

                    var body = {
                        address: formattedAddress,
                        latCoord: lat,
                        lngCoord: lng
                    }

                    console.log(formData)

                    axios({
                        method: 'post',
                        url: '/account/setaddress',
                        data: body,
                       
                    })
                        .then(function (response) {
                            console.log(response);
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                })
               
               

                
                value = response.data.results[i].geometry.location;

            }

            

        })
        .catch(function (error) {
            // handle error
            console.log(error);
        })

}