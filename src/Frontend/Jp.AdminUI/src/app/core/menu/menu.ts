export class MenuItem {
    constructor() { }
    text: string;
    heading?: boolean;
    link?: string;     // internal route links
    elink?: string;   // used only for external links
    target?: string;   // anchor target="_blank|_self|_parent|_top|framename"
    icon?: string;
    alert?: string;
    submenu?: Array<any>;
    lightVersion?: boolean;
}

const Home: MenuItem = {
    text: "Home",
    link: "/home",
    icon: "icon-home"
};

const Clients: MenuItem  = {
    text: "Clients",
    link: "/clients",
    icon: "fa fa-desktop",
    submenu: [
        {
            text: "List",
            link: "/clients"
        },
        {
            text: "Add",
            link: "/clients/add"
        }
    ]
};

const IdentityResource: MenuItem  = {
    text: "Identity Resources",
    link: "/identity-resource",
    icon: "far fa-id-card",
    submenu: [
        {
            text: "List",
            link: "/identity-resource"
        },
        {
            text: "Add",
            link: "/identity-resource/add"
        }
    ]
};

const ApiResource: MenuItem  = {
    text: "Api Resources",
    link: "/api-resource",
    icon: "fas fa-cloud",
    submenu: [
        {
            text: "List",
            link: "/api-resource"
        },
        {
            text: "Add",
            link: "/api-resource/add"
        }
    ]
};

const PersistedGrants: MenuItem  = {
    text: "Persisted Grants",
    link: "/persisted-grants",
    icon: "fas fa-key"
};

const Users: MenuItem  = {
    text: "Users",
    link: "/users",
    icon: "fas fa-users-cog",
    submenu: [
        {
            text: "List",
            link: "/users"
        },
        {
            text: "Add",
            link: "/users/add"
        }
    ],
    lightVersion: false
};

const Roles: MenuItem  = {
    text: "Roles",
    link: "/roles",
    icon: "fas fa-user-tag",
    submenu: [
        {
            text: "List",
            link: "/roles"
        },
        {
            text: "Add",
            link: "/roles/add"
        }
    ],
    lightVersion: false
};

const headingMain: MenuItem  = {
    text: "IdentityServer4",
    heading: true
};

const headingUsers: MenuItem  = {
    text: "Users",
    heading: true,
    lightVersion: false
};
const headingSettings: MenuItem  = {
    text: "SSO Settings",
    heading: true,
    lightVersion: false
};

export const menu: MenuItem[]  = [
    headingMain,
    Home,
    Clients,
    IdentityResource,
    ApiResource,
    PersistedGrants,
    headingUsers,
    Users,
    Roles,
    headingSettings
];
