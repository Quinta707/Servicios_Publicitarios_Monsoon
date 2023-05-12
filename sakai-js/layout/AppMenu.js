import React, { useState, useEffect, useContext, useRef } from 'react';
import AppMenuitem from './AppMenuitem';
import { LayoutContext } from './context/layoutcontext';
import { MenuProvider } from './context/menucontext';
import Link from 'next/link';

import { useRouter } from 'next/router'


const AppMenu = () => {

    const router = useRouter();
    const { layoutConfig } = useContext(LayoutContext);

    // en estos tres se guardan los arreglos dentro de un drop down list
    const Publicidad = [];
    const General = [];
    const Acceso = [];

    // en estas tres arreglo estan las dirreciones de la páginas 
    const pbliItems = [];
    const accItems = [];
    const gralItems = [];

    const [posts, setPosts] = useState([]);
    const [model1, setmodel1] = useState([]);

    useEffect(() => {

        if (localStorage.getItem('usuID') == "" || localStorage.getItem('usuID') == null) {
            console.log(localStorage.getItem('usuID'));
            router.push('/auth/login');
        }
        else {

            var role_Id = localStorage.getItem('role_Id');

            if (localStorage.getItem('user_EsAdmin') == "true") {
                var EsAdmin = 1;
            }
            else {
                var EsAdmin = 0;
            }

            const url = 'https://localhost:44304/api/Pantalla/PantallaMenu?id=' + role_Id + '&EsAdmin=' + EsAdmin;
            fetch(url)
                .then(response => response.json())
                .then(data => {
                    setPosts(data.data)
                    console.log(data)
                })
        }

    }, []);


    const acce = (item) => {
        if (item.length > 0) {
            Acceso.push({
                label: 'Acceso',
                icon: 'pi pi-fw pi-user',
                items: item, // Sin descomponer item en otro array
            });
        }
    }

    const pbli = (item) => {
        if (item.length > 0) {
            Publicidad.push({
                label: 'Publicidad',
                icon: 'pi pi-fw pi-user',
                items: item, // Sin descomponer item en otro array
            });
        }
    };

    const gral = (item) => {
        if (item.length > 0) {
            General.push({

                label: 'General',
                icon: 'pi pi-fw pi-user',
                items: item, // Sin descomponer item en otro array
            });
        }
    }

    posts.forEach((post) => {
        if (post.pant_Menu === 'Publicidad') {
            pbliItems.push({
                label: post.pant_Nombre,
                icon: post.pant_Icono,
                to: post.pant_Url
            });
        } else if (post.pant_Menu === 'Acceso') {
            accItems.push({
                label: post.pant_Nombre,
                icon: post.pant_Icono,
                to: post.pant_Url
            });
        } else {
            gralItems.push({
                label: post.pant_Nombre,
                icon: post.pant_Icono,
                to: post.pant_Url
            });
        }
    });

    pbli(pbliItems); // Llamar a la función pbli con los elementos correspondientes
    acce(accItems); // Llamar a la función acce con los elementos correspondientes
    gral(gralItems); // Llamar a la función gral con los elementos correspondientes


    const updatedModel1 = [
        ...model1,
        {
            label: '********** Publicidad Menu **********',
            items: [{ label: 'Home', icon: 'pi pi-fw pi-home', to: '/' }]
        },
        {
            items: [
                ...Publicidad,
            ]
        },
        {
            items: [
                ...Acceso,
            ]
        },
        {
            items: [
                ...General,
            ]
        },

    ];






    // const updatedModel1 = model1.concat(
    //     posts.map((post) => {

    //         return {
    //             label: post.pant_Menu,
    //             items: [
    //                 {
    //                     label: post.pant_Nombre,
    //                     icon: post.pant_Icono,
    //                     to: post.pant_Url
    //                 }
    //             ]
    //         };
    //     })
    // );

    // console.log(updatedModel1)

    return (
        <MenuProvider>
            <ul className="layout-menu">
                {updatedModel1.map((item, i) => {
                    return !item.seperator ? <AppMenuitem item={item} root={true} index={i} key={item.label} /> : <li className="menu-separator"></li>;
                })}
            </ul>
        </MenuProvider>
    );
};

export default AppMenu;
