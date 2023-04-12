// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: true,
  clientId: 'b096cef4-7241-4cda-9e52-9f9f81cc2c25',
  authority: 'https://devcrowdb2c.b2clogin.com/devcrowdb2c.onmicrosoft.com/B2C_1_signin',
  authorityDomain: 'devCrowdB2C.b2clogin.com',
  redirectUri: 'http://localhost:4200',
  endpoint: 'http://localhost:5229',
  pathSearch: '/search',
  pathPerson: '/person',
  endpointScopes: [ 'https://devcrowdb2c.onmicrosoft.com/67ed75a2-3ff2-46da-8dae-ebe4f34f8901/all' ]
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
