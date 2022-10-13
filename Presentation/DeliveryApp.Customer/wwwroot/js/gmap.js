let Apikey = 'AIzaSyCFpqjXIt_kVyWqK3_9mrCXD5_hASViWus';
let gmap = document.getElementById("gmap");
let gmap_area = document.getElementById("gmaparea");
let use_location = document.querySelectorAll(".use_location");


gmap.onkeyup = function () {
    

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
                gmap_area.classList.remove("d-none");
                gmap_area.innerHTML = "";
                var formattedAddress = response.data.results[i].formatted_address;

                let a = document.createElement("a");
                a.classList.add("text-decoration-none", "use_location");

                let p = `<p class="font-weight-bold text-primary m-0"><i class="feather-navigation"></i> ${formattedAddress}</p>`;
                a.innerHTML = p;
                gmap_area.innerHTML += `<a href="#" class="text-decoration-none use_location mb-1 uselocations">
                                        <p class="font-weight-bold text-primary m-0"><i class="feather-navigation"></i> ${formattedAddress}</p>
                                        </a>`;
                value = response.data.results[i].geometry.location;
                
            }

            let uselocations = document.querySelectorAll(".uselocations");

            uselocations.forEach(x => {
                x.addEventListener("click", function () {
                    localStorage.setItem("uselocation", JSON.stringify(value))
                })
            })

        })
        .catch(function (error) {
            // handle error
            console.log(error);
        })

}

