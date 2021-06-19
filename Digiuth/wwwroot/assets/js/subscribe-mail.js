let subscribeBtn = document.getElementById('subscribeBtn');

subscribeBtn.addEventListener("click", function (e) {
    e.preventDefault();
    let email = document.getElementById('inputEmail').value;
    
    if (!ValidateEmail(email)) {
        return;
    };
    let formdata = new FormData();
    formdata.append("email", email);
    axios.post('/Home/Subscribe', formdata)
        .then(function (response) {
            
            if (response.data) {
                swal({
                    title: "Please enter another email",
                    text: "This email already exists!",
                    icon: "warning",
                });
            } else {
                swal({
                    title: "Thanks for subscribing",
                    text: "You subscribed successfully!",
                    icon: "success",
                });
            }
        })
        .catch(function (error) {
            if (error.response) {
                let errorObj = error.response.data;
                for (let errors in errorObj) {
                    let error = errorObj[errors];
                    console.log(error);
                }
            }
        });
})

function ValidateEmail(mail) {
    if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(mail)) {
        return (true)
    } else {
        swal({
            title: "Please enter email",
            text: "You have entered an invalid email address!",
            icon: "error",
        });
        return (false)
    }
}