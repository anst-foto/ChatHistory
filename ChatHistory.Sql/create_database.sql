CREATE TABLE table_users
(
    id        INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name      TEXT    NOT NULL,
    is_delete INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE table_history
(
    id               INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    sender_id        INTEGER NOT NULL,
    receiver_id      INTEGER NOT NULL,
    message          TEXT    NOT NULL,
    reply_message_id INTEGER NULL,
    date_send        TEXT    NOT NULL,
    is_receive       INTEGER NOT NULL DEFAULT 0,
    is_read          INTEGER NOT NULL DEFAULT 0,
    is_delete        INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY (sender_id) REFERENCES table_users (id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    FOREIGN KEY (receiver_id) REFERENCES table_users (id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    FOREIGN KEY (reply_message_id) REFERENCES table_history (id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);