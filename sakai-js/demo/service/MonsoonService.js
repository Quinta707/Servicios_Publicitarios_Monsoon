export const MonsoonService = {
    getMonsoonService() {
        return fetch('https://localhost:44304/api/Servicio/Listado', { headers: { 'Cache-Control': 'no-cache' } })
            .then((res) => res.json())
            .then((d) => d.data);
    }
};
