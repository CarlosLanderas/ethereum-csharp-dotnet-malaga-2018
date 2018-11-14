import React, { Fragment } from "react";
import styles from "./styles";
export const Header = () => {
    return (
        <Fragment>
            <h1 style={styles.header} className="jumbotron">
                Blockchain Maintenance
            </h1>
        </Fragment>
    )
}