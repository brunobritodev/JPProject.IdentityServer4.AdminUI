import { Claim } from './claim.model';

export class Ldap{
    domainName: string;
    distinguishedName: string;
    authType: string;
    searchScope: string;
}

export class LdapConnectionResult {
    public success: boolean;
    public claims: Claim[];
    public error: string;
    public step: string;
}