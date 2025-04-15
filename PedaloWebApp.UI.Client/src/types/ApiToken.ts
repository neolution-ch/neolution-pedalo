export interface ApiToken {
  unique_name: string;
  name: string;
  jti: string;
  amr: string;
  family_name: string;
  given_name: string;
  tenantid: string;
  accountid: string;
  lang: string;
  TenantsAdmin: string;
  nbf: number;
  exp: number;
  iat: number;
}
