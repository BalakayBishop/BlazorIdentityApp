﻿
// home.js
var swRegistration = null;

function GetSubscription() {
    // Check for pushManager subscription
    swRegistration.pushManager.getSubscription()
        .then(function (subscription) {
            var isSubscribed = !(subscription === null);

            // If the user is already subscribed
            if (isSubscribed) {
                console.log('User IS subscribed.');
                console.log(subscription);
                sessionStorage.setItem("subscribed", "subscribed");
                var email = $('#currentUserEmail').attr('data-currentUser');
                // call Blazor to update the db record
                DotNet.invokeMethodAsync('BlazorIdentityApp.Client', 'hCreateOrUpdatePushSubscription', JSON.stringify(subscription), email)
                    .then(response => {
                        console.log(response);
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            }
            else {
                // Set session item to indicate user is not subscribed for other js files to read
                console.log('User is NOT subscribed.');
                sessionStorage.setItem("subscribed", "not subscribed");
            }
        });
}

function CheckOnLoad() {
    if ('serviceWorker' in navigator && 'PushManager' in window && "Notification" in window) {
        // All features supported
        sessionStorage.setItem("support", "supported");
        
        navigator.serviceWorker.register('sw.js')
            .then(function (swReg) {
                console.log('Service Worker is registered', swReg);
                sessionStorage.setItem("sw", "registered");
                swRegistration = swReg;
                GetSubscription();
            })
            .catch(function (error) {
                console.error('Service Worker Error', error);
                sessionStorage.setItem("sw", "not registered");
            });
    }
    else {
        // All features not supported
        sessionStorage.setItem("support", "not supported");
    }
}

// Calling this function from Blazor
async function checkFirstVisit() {
    // grab session item
    var isFirstVisit = sessionStorage.getItem('firstVisit');

    // if null, session item does not exist yet
    if (isFirstVisit == null) {
        sessionStorage.setItem('firstVisit', 'false');
        console.log('First visit this session');
        CheckOnLoad();
    }
    // if not null and equal to false this is second visit
    else if (isFirstVisit === 'false') {
        console.log('Returning visit this session');
    }
}