import { Claim } from './claim.model';

export class Ldap{
    address: string;
    domainName: string;
    portNumber: number;
    distinguishedName: string;
    authType: string;
    searchScope: string;
    fullyQualifiedDomainName: boolean;
    connectionLess: boolean;
}

export class LdapConnectionResult {
    public success: boolean;
    public claims: Claim[];
    public error: string;
    public step: string;
}