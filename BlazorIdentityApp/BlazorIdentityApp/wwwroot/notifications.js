
'use strict';

/// <summary>
/// Converts a base64 string to a Uint8Array.
/// This method is primarily used for converting a base64 encoded string, such as a public VAPID key from a push service,
/// into a Uint8Array. The resulting array is then utilized for subscribing to push notifications.
/// </summary>
/// <param name="base64String">The base64 encoded string to be converted.</param>
/// <returns>A Uint8Array representing the converted base64 string.</returns>

function urlB64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

/// <summary>
/// Updates the subscription details on the server and updates the UI accordingly.
/// This function sends the subscription object to the server using an asynchronous call. It also updates the UI
/// to display the subscription details if the subscription is successful, or hides the details if not.
/// </summary>
/// <param name="subscription">The subscription object to be sent to the server.</param>
function updateSubscriptionOnServer(subscription) {
    var email = $('#currentUserEmail').attr('data-currentUser');
    DotNet.invokeMethodAsync('BlazorIdentityApp.Client', 'nCreateOrUpdatePushSubscription', JSON.stringify(subscription), email)
        .then(response => {
            console.log(response);
        })
        .catch(error => {
            console.error('Error:', error);
        });

    const subscriptionJson = document.querySelector('.js-subscription-json');
    const subscriptionDetails =
        document.querySelector('.js-subscription-details');

    if (subscription) {
        subscriptionJson.textContent = JSON.stringify(subscription);
        subscriptionDetails.classList.remove('is-invisible');
    } else {
        subscriptionDetails.classList.add('is-invisible');
    }
}

/// <summary>
/// Subscribes the user to push notifications.
/// This function converts the application server's public key to a Uint8Array, checks if the service worker is ready,
/// and then subscribes the user to push notifications using the push manager. If the subscription is successful,
/// it updates the subscription on the server. If there is an error during subscription, it logs the error and updates the UI accordingly.
/// </summary>
function subscribeUser() {
    // Public Key from Firebase Cloud Messaging
    const applicationServerPublicKey = 'BFKuJ6J0cxmJAtBYDazFjSzBtyg6MajHbfnwBXsfFkZDmu8vQgtv-hKPNggwEBqpF2qquxguBHMoGh-65_96ACI';
    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);

    navigator.serviceWorker.ready.then(function (registration) {
        registration.pushManager.subscribe({
            userVisibleOnly: true,
            applicationServerKey: applicationServerKey
        })
            .then(function (subscription) {
                if (subscription !== null) {
                    console.log('User is subscribed');
                    updateSubscriptionOnServer(subscription);
                }
            })
            .catch(function (err) {
                console.log('Failed to subscribe the user: ', err);
                updateBtn();
            });
    });
}

/// <summary>
/// Initializes the UI for push notification subscription.
/// This function sets up the UI for push notification subscription by adding an event listener to the push button.
/// When the button is clicked, it checks if the user is already subscribed or if push notifications are supported.
/// Based on these checks, it either subscribes the user or updates the button text accordingly.
/// </summary>
function initializeUI() {
    console.log("initializeUI called")
    var pushButton = document.querySelector('.js-push-btn');
    pushButton.addEventListener('click', function () {
        pushButton.disabled = true;

        const isSubscribed = sessionStorage.getItem("subscribed");
        const isSupported = sessionStorage.getItem("support");

        if (isSubscribed === 'subscribed') {
            console.log("User is subscribed")
            pushButton.textContent = 'User Already Subsribed';
        }
        else {
            if (isSupported === 'supported') {
                console.log("Feature support is present")
                subscribeUser();
            }
        }
    });

    pushButton.textContent = 'Enable Push Notifications';
    pushButton.disabled = false;
}

$(document).ready(function () {
    initializeUI();
});