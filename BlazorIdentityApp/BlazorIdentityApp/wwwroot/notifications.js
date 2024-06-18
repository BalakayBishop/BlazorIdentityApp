﻿
'use strict';

// Public Key from Firebase Cloud Messaging
const applicationServerPublicKey = 'BFKuJ6J0cxmJAtBYDazFjSzBtyg6MajHbfnwBXsfFkZDmu8vQgtv-hKPNggwEBqpF2qquxguBHMoGh-65_96ACI';

const pushButton = document.querySelector('.js-push-btn');

let isSubscribed = false;
let swRegistration = null;

// Convert key
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

// Update button UI
function updateBtn() {
    if (Notification.permission === 'denied') {
        pushButton.textContent = 'Push Messaging Blocked';
        pushButton.disabled = true;
        //updateSubscriptionOnServer(null);
        return;
    }

    if (isSubscribed) {
        pushButton.textContent = 'Disable Push Messaging';
    } else {
        pushButton.textContent = 'Enable Push Messaging';
    }

    pushButton.disabled = false;
}

function updateSubscriptionOnServer(subscription) {

    DotNet.invokeMethodAsync('BlazorWasmPwaPoc.Client', 'nCreateOrUpdatePushSubscription', JSON.stringify(subscription))
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

function subscribeUser() {
    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
    swRegistration.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: applicationServerKey
    }).then(function (subscription) {
        console.log('User is subscribed');
        if (!(subscription === null)) {
            updateSubscriptionOnServer(subscription);
        }

        isSubscribed = true;

        updateBtn();
    }).catch(function (err) {
        console.log('Failed to subscribe the user: ', err);
        updateBtn();
    });
}

function initializeUI() {
    pushButton.addEventListener('click', function () {
        pushButton.disabled = true;
        if (isSubscribed) {
            // TODO: Unsubscribe user
        } else {
            subscribeUser();
        }
    });
}