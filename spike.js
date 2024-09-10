import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        { duration: '5s', target: 5000 },
        { duration: '5s', target: 5000 },
        { duration: '5s', target: 0 },
    ],
};

export default function () {
    const url = 'http://localhost:5249/urls/test';

    let response = http.get(url);

    check(response, {
        'status is redirect': (r) => r.status === 301,
    });

    sleep(1); 
}