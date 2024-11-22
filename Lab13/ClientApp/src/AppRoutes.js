import HospitalList from "./components/HospitalList";
import HospitalSearch from "./components/HospitalSearch";
import RosterList from "./components/RosterList";
import RosterSearch from "./components/RosterSearch";
import WardList from "./components/WardList";
import WardSearch from "./components/WardSearch";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/hospitals',
        element: <HospitalList />
    },
    {
        path: '/hospital-search',
        element: <HospitalSearch />
    },
    {
        path: '/rosters',
        element: <RosterList />
    },
    {
        path: '/roster-search',
        element: <RosterSearch />
    },
    {
        path: '/wards',
        element: <WardList />
    },
    {
        path: '/ward-search',
        element: <WardSearch />
    }
];

export default AppRoutes;
