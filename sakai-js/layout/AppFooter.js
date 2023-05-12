import React, { useContext } from 'react';
import { LayoutContext } from './context/layoutcontext';

const AppFooter = () => {
    const { layoutConfig } = useContext(LayoutContext);

    return (
        <div className="layout-footer">
            <img src={`https://i.pinimg.com/originals/aa/4c/06/aa4c0662450520592ce81df389c4b60f.jpg`} alt="Logo" height="20" className="mr-2" />
            Por
            <span className="font-medium ml-2">Servicios Publicitarios Moonson</span>
        </div>
    );
};

export default AppFooter;
