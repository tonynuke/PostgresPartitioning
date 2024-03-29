﻿CREATE TABLE IF NOT EXISTS purchases
(
    id uuid NOT NULL,
    person_id bigint NOT NULL,
    date_time timestamp with time zone NOT NULL,
    CONSTRAINT pk_purchases PRIMARY KEY (date_time, id),
    CONSTRAINT fk_purchases_persons_person_id FOREIGN KEY (person_id)
        REFERENCES public.persons (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
) PARTITION BY RANGE (date_time);

CREATE TABLE purchases_default PARTITION OF purchases DEFAULT;

CREATE TABLE purchases_2022 PARTITION OF purchases
    FOR VALUES FROM ('2022-01-01') TO ('2023-01-01');

CREATE TABLE purchases_2023 PARTITION OF purchases
    FOR VALUES FROM ('2023-01-01') TO ('2024-01-01');

    CREATE TABLE purchases_2024 PARTITION OF purchases
    FOR VALUES FROM ('2024-01-01') TO ('2025-01-01');

CREATE INDEX ON purchases (date_time, person_id);