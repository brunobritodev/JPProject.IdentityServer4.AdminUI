
const Home = {
    text: "Home",
    link: "/home",
    icon: "icon-home"
};

const Settings = {
    text: "Clients",
    link: "/clients",
    icon: "fa fa-desktop",
    submenu: [
        {
            text: "List",
            link: "/clients/list"
        },
        {
            text: "Add",
            link: "/clients/add"
        }
    ]
};

const IdentityResource = {
    text: "Identity Resources",
    link: "/identity-resource",
    icon: "far fa-id-card",
    submenu: [
        {
            text: "List",
            link: "/identity-resource/list"
        },
        {
            text: "Add",
            link: "/identity-resource/add"
        }
    ]
};

const ApiResource = {
    text: "Api Resources",
    link: "/api-resource",
    icon: "fas fa-cloud",
    submenu: [
        {
            text: "List",
            link: "/api-resource/list"
        },
        {
            text: "Add",
            link: "/api-resource/add"
        }
    ]
};

const PersistedGrants = {
    text: "Persisted Grants",
    link: "/persisted-grants",
    icon: "fas fa-key"
};

const Users = {
    text: "Users",
    link: "/users",
    icon: "fas fa-users-cog",
    submenu: [
        {
            text: "List",
            link: "/users/list"
        },
        {
            text: "Add",
            link: "/users/add"
        }
    ]
};

const Roles = {
    text: "Roles",
    link: "/roles",
    icon: "fas fa-user-tag",
    submenu: [
        {
            text: "List",
            link: "/roles/list"
        },
        {
            text: "Add",
            link: "/roles/add"
        }
    ]
};

const headingMain = {
    text: "Main Navigation",
    heading: true
};

export const menu = [
    headingMain,
    Home,
    Settings,
    IdentityResource,
    ApiResource,
    PersistedGrants,
    Users,
    Roles
];
