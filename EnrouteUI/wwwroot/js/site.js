window.getCoords = async () => {
    const pos = await new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(resolve, reject);
    });

    var obj = {
        Lng: pos.coords.longitude,
        Lat: pos.coords.latitude
    };

    return obj;

};


window.showFailToast = async (message) => {
    Toastify({
        text: message,
        duration: 9000,
        close: true,
        gravity: "top",
        position: "right",
        stopOnFocus: true,
        style: {
            background: "linear-gradient(to right, #FF0000, #FF0000)"
        },

        onClick: function () { }
    }).showToast();
};