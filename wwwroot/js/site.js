// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function initPayPalButton(fname, lname, phone) {
  paypal
    .Buttons({
      style: {
        shape: "rect",
        color: "gold",
        layout: "vertical",
        label: "checkout",
      },

      createOrder: function (data, actions) {
        return actions.order.create({
          purchase_units: [
            {
              description: "Reserve a Purebred Goldendoodle Puppy",
              amount: {
                currency_code: "USD",
                value: 537.5,
                breakdown: {
                  item_total: { currency_code: "USD", value: 500 },
                  shipping: { currency_code: "USD", value: 0 },
                  tax_total: { currency_code: "USD", value: 37.5 },
                },
              },
            },
          ],
        });
      },

      onApprove: function (data, actions) {
        return actions.order.capture().then(function (orderData) {
          // Full available details
          console.log(
            "Capture result",
            orderData,
            JSON.stringify(orderData, null, 2)
          );

          // Show a success message within this page, e.g.
          const element = document.getElementById("paypal-button-container");
          element.innerHTML = "";
          element.innerHTML = "<h3>Thank you for your payment!</h3>";

          // Call CreateWaitlistEntry Stored Procedure
          $.post( `/create?firstName=${fname}&lastName=${lname}&phoneNumber=${phone}` );
        });
      },

      onError: function (err) {
        console.log(err);
      },
    })
    .render("#paypal-button-container");
}

function validateInput() {
  const fname = $("#fname").val();
  const lname = $("#lname").val();
  const phone = $("#phone").val();

  if (!fname || !lname || !phone) {
    alert(
      "All form fields are required. Please complete the form and try again."
    );
    return;
  }

  const regexp = /^([0-9]){10}$/;

  if (regexp.test(phone)) {
    initPayPalButton(fname, lname, phone);
  } else {
    alert(
      "Phone number must be exactly 10 digits, no letters or symbols. Please edit the form and try again."
    );
    return;
  }
}
