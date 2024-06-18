
// home.js

function CheckOnLoad() {
    if ('serviceWorker' in navigator && 'PushManager' in window && "Notification" in window) {
        // All features supported
        sessionStorage.setItem("support", "supported");

        // Check for pushManager subscription
        swRegistration.pushManager.getSubscription()
            .then(function (subscription) {
                var isSubscribed = !(subscription === null);

                // If the user is already subscribed, call Blazor to update the db record
                if (isSubscribed) {
                    console.log('User IS subscribed.');
                    sessionStorage.setItem("subscribed", "subscribed");
                    DotNet.invokeMethodAsync('BlazorWasmPwaPoc.Client', 'hCreateOrUpdatePushSubscription', JSON.stringify(subscription))
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
    else {
        // All features not supported
        sessionStorage.setItem("support", "not supported");
    }
}

function checkFirstVisit() {
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

// When the page loads call function to check if first visit
window.onload = checkFirstVisit;