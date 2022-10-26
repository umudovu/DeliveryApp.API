let map, infoWindow;

const formDataArea = document.getElementById("formDataArea");
const collapseOne = document.getElementById("collapseOne");

const phoneNumber = document.getElementById("phoneNumber");
const locationButton = document.getElementById("custom-map-control-button"); //document.createElement("button");


phoneNumber.onchange = function () {
    locationButton.removeAttribute("disabled");
}

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: 40.4241061, lng: 49.8226989 },
        zoom: 15,
    });
    infoWindow = new google.maps.InfoWindow();



    //locationButton.textContent = "Pan to Current Location";
    //locationButton.classList.add("custom-map-control-button");
    // map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);
    locationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,

                    };

                    var latitude = position.coords.latitude;
                    var longitude = position.coords.longitude;
                    let address = "";

                    infoWindow.setPosition(pos);
                    infoWindow.setContent("You");
                    infoWindow.open(map);
                    map.setCenter(pos);
                    console.log(position.coords)

                    localStorage.setItem("uselocation", JSON.stringify(pos));

                    ConfirmOrder(latitude, longitude);

                    var latlng = new google.maps.LatLng(latitude, longitude);
                    console.log(latlng)
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());

        }
    });
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}

window.initMap = initMap;

$(window).load(function () {

    var phones = [{ "mask": "(###) ###-##-##" }];
    $('#phoneNumber').inputmask({
        mask: phones,
        greedy: false,
        definitions: { '#': { validator: "[0-9]", cardinality: 1 } }
    });
});

function ConfirmOrder(latitude, longitude) {


    axios.get(`/order/confirm?phoneNumber=${phoneNumber.value}&latitude=${latitude}&longitude=${longitude}`)
        .then(function (response) {
            // handle success
            //console.log(response);
            formDataArea.removeAttribute("class");
        })
        .catch(function (error) {
            // handle error
            console.log(error);
        })

}
